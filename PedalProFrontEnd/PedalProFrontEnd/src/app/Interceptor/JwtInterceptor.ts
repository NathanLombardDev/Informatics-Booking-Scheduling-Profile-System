import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Retrieve the JWT from local storage
    const jwt = localStorage.getItem('jwt');

    // If the JWT is available, add it to the request header
    if (jwt) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jwt}`
        }
      });
    }

    // Continue with the modified request
    return next.handle(request);
  }
}