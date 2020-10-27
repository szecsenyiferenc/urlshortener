import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

constructor(private http: HttpClient) { }

public createUrl(url: string): Observable<string> {
  return this.http.post('api/url', url, {responseType: 'text'});
}

public getUrl(url: string): Observable<string> {
  return this.http.get(`api/url/${url}`, {responseType: 'text'});
}

}
