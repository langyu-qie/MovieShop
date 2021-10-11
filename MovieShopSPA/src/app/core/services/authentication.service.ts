import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();

  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  login(userLogin: Login): Observable<boolean> {

    return this.http.post(`${environment.apiUrl}account/login`, userLogin)
      .pipe(map((response: any) => {
        if (response) {
          // Save JWT sent from server in localstorage
          localStorage.setItem('token', response.token);
          this.populateUserInfo();
          return true;
        }
        return false;
      }));
  }

  // Verify JWT in localstorage with server & load user's info.
  // This runs once on application startup.

  populateUserInfo() {

    var token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {

      const decodedToken = this.jwtHelper.decodeToken(token);
      // Set current user data into observable
      this.currentUserSubject.next(decodedToken);
      // Set isAuthenticated to true
      this.isLoggedInSubject.next(true);

    }
  }

  logout() {
    // Remove JWT from localstorage
    localStorage.removeItem('token');
    // Set current user to an empty object
    this.currentUserSubject.next({} as User);
    // Set auth status to false
    this.isLoggedInSubject.next(false);

  }
}
