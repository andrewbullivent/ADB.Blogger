import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Subject, map, Observable, catchError, retry, throwError, of, switchMap } from 'rxjs';
import Config  from '../../assets/config/app.config.json'
import { UserInfo } from '../models/userlogin';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private loginUrl = '/login?useCookies=true';
  private logoutUrl = '/logout';
  private userInfoUrl = '/manage/info'
  private rolesUrl = '/roles';
  private baseUrl = Config.appSettings.apiBaseUrl;

  constructor(private http: HttpClient) { }

  private _authStateChanged: Subject<boolean> = new BehaviorSubject<boolean>(false);

  public onStateChanged() {
    return this._authStateChanged.asObservable();
  }

  public login(email:string, password:string){
    const loginData = {email, password};
    return this.http.post(
      `${this.baseUrl}${this.loginUrl}`, 
      loginData,
      {
        observe: 'response', 
        responseType: 'text',
        withCredentials:true,
        headers: {
          'accept': 'application/json',
          'Content-Type': 'application/json'          
        }
      })
    .pipe(
      map((res:HttpResponse<string>)=>
      {
        this._authStateChanged.next(res.ok);
        return res.ok;
      }),
      retry(3),
      catchError(this.handleError)
    );
  }

  // sign out
  public logout() {
    return this.http.post(`${this.baseUrl}${this.logoutUrl}`, {}, {
      withCredentials:true,
      observe: 'response',
      responseType: 'text'
    }).pipe<boolean>(map((res: HttpResponse<string>) => {
      if (res.ok) {
        this._authStateChanged.next(false);
      }
      return res.ok;
    }));    
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

    // check if the user is authenticated. the endpoint is protected so 401 if not.
    public user() {
      return this.http.get<UserInfo>(`${this.baseUrl}${this.userInfoUrl}`, {
      withCredentials:true
    }).pipe(
        switchMap(userInfo => {
          return this.http.get<string[]>(`${this.baseUrl}${this.rolesUrl}`)
          .pipe(map(roles=>{
            return {
              email: userInfo.email,
              isEmailConfirmed: userInfo.isEmailConfirmed,
              roles: roles
            }
          }))
        }),
        catchError((error: HttpErrorResponse) => {
          if(error.status == 401){
            console.log("User is not authenticated.");
          }
          return of({} as UserInfo);
        }));
    }
  
    // is signed in when the call completes without error and the user has an email
    public isSignedIn(): Observable<boolean> {
      return this.user().pipe(
        map((userInfo) => {
          const valid = !!(userInfo && userInfo.email && userInfo.email.length > 0);
          return valid;
        }),
        catchError(() => {
          return of(false);
        }));
    }

    public roles(): Observable<string[]> {
      return this.http.get<string[]>(`${this.baseUrl}${this.rolesUrl}`, {
        withCredentials:true
      }).pipe(
          catchError(() => {
            return of({} as string[]);
          }));
    }
  }

  

