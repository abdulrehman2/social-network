import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JWTInterceptor implements HttpInterceptor {
  //constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    newRequest: HttpHandler
  ): Observable<HttpEvent<any>> {
    let token = localStorage.getItem('jwt');
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
          // 'Content-Type': 'application/json',
          // Accept: 'application/json',
        },
      });
    }
    return newRequest.handle(request);
  }
}
