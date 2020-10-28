import { BackendService } from './../shared/services/backend.service';
import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { catchError, filter, takeUntil, tap } from 'rxjs/operators';
import { of, Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  isReplaced: boolean;
  errorOccured: boolean;

  form = new FormGroup({
    'currentUrl': new FormControl('',
    [
      Validators.required,
      Validators.pattern('(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?')
    ])
  });

  @HostListener('copy', ['$event'])
    handleCopy(e) {
      e.clipboardData.setData('text/plain', this.currentUrl.value);
      e.preventDefault();
    }

  constructor(private backendService: BackendService) {
    this.isReplaced = false;
    this.errorOccured = false;

    this.form.controls['currentUrl'].valueChanges.pipe(
      takeUntil(this.destroy$),
      filter(() => this.isReplaced),
      tap(() => this.isReplaced = false)
    ).subscribe();
  }



  get currentUrl() {
    return this.form.get('currentUrl');
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit() {
    console.log(this.currentUrl.value);
    this.backendService.createUrl(this.currentUrl.value).pipe(
      tap(url => {
        this.currentUrl.reset();
        this.currentUrl.setValue(url);
        this.isReplaced = true;
        this.errorOccured = false;
      }),
      catchError(() => {
        this.errorOccured = true;
        return of(null);
      })
    ).subscribe();
  }

  copy() {
    document.execCommand('copy');
  }

}
