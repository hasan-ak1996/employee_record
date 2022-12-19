import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import Swal from 'sweetalert2';
@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor{

  constructor() { }
  intercept(req: HttpRequest<any>,
     next: HttpHandler
     ): Observable<HttpEvent<any>> {
      const hardCodedToken = localStorage.getItem('token');
      req = req.clone({

        setHeaders: {
          Authorization: `Bearer ${hardCodedToken}`,
        },
      });
      return next.handle(req).pipe(
        // retry(2),
        catchError((error: HttpErrorResponse) => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.error.message,
          })
          return throwError(() => new Error(error.error.message));
        })
      );
  }
}
