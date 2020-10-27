import { BackendService } from './../shared/services/backend.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.scss']
})
export class RedirectComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private backendService: BackendService) { }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(param => this.backendService.getUrl(param.get('id'))),
      tap(url => window.location.href = url),
      catchError(() => this.router.navigateByUrl('/notfound'))
    )
    .subscribe();
  }

}
