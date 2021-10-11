import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map, take } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanLoad {
  constructor(private authService: AuthenticationService, private router: Router) {

  }
  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    //  return this.authService.isLoggedIn.pipe(take(1));
    const url = `/${route.path}`;
    // console.log('inside canLoad');
    // console.log(url);
    // console.log(route);
    return this.checkLogin(url);

  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

    return this.checkLogin(state.url);
  }

  checkLogin(url: string): Observable<boolean> {
    return this.authService.isLoggedIn.pipe(map(response => {
      if (response) {
        return true
      } else {
        this.router.navigate(["/account/login"], {
          queryParams: { returnUrl: url }
        });
        return false;
      }
    }),
      catchError(() => {
        this.router.navigate(["/login"]);
        return of(false);
      })

    )

  }

}
