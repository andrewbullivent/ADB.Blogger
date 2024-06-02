import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, retry, throwError } from 'rxjs';
import Config  from '../../assets/config/app.config.json'
import { NewPostViewModel, Post, PostViewModel } from '../models/post.model';
import { StateResult } from '../models/state-result.model';

@Injectable({
  providedIn: 'root'
})
export class BloggerApiService {


  private postUrl = '/api/v1/posts';
  private tagUrl = '/api/v1/tags';
  private baseUrl = Config.appSettings.apiBaseUrl;
  constructor(private http: HttpClient) { }

  getPosts(): Observable<StateResult<Post[]>> {
    return this.http.get<StateResult<Post[]>>(`${this.baseUrl}${this.postUrl}`,{ withCredentials: true})
    .pipe(
      retry(3),
      catchError(this.handleError)
    );
  }

  getPost(postId: string): Observable<StateResult<Post>> {
    return this.http.get<StateResult<Post>>(`${this.baseUrl}${this.postUrl}/${postId}`,
    {
      withCredentials:true,
      headers: {
        'accept': 'application/json',
        'Content-Type': 'application/json'          
      }
    }
    )
    .pipe(
      retry(3),
      catchError(this.handleError)
    );
  }

  savePost(post:NewPostViewModel): Observable<Post> {
    return this.http.post<Post>(`${this.baseUrl}${this.postUrl}`,post,
    {
      withCredentials:true,
      headers: {
        'accept': 'application/json',
        'Content-Type': 'application/json'          
      }
    }
    )
    .pipe(
      retry(3),
      catchError(this.handleError)
    );

  }


  updatePost(post: PostViewModel): Observable<Post> {
    return this.http.put<Post>(`${this.baseUrl}${this.postUrl}/${post.id}`,post,
    {
      withCredentials:true,
      headers: {
        'accept': 'application/json',
        'Content-Type': 'application/json'          
      }
    }
    )
    .pipe(
      retry(3),
      catchError(this.handleError)
    );
  }

  deletePost(id: string) {
    return this.http.delete(`${this.baseUrl}${this.postUrl}/${id}`,
    {
      withCredentials:true,
      headers: {
        'accept': 'application/json',
        'Content-Type': 'application/json'          
      }
    }
    )
    .pipe(
      retry(3),
      catchError(this.handleError)
    );
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
}
