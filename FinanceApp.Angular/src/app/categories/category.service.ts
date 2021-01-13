import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Category } from './category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }
  
  private categoryUrl = 'https://localhost:44313/api/categories';

  headers = new HttpHeaders({ 'content-type': 'application/json','Access-Control-Allow-Origin': '*'});
  httpOptions = {
    headers: this.headers,
    crossDomain: true
  };

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.categoryUrl);
  };

   getCategory(id: number) {
     const url = `${this.categoryUrl}/${id}`;
    return this.http.get<Category>(url).pipe();
   };
 
  addCategory(category: Category): Observable<Category> {
    console.log("addTransaction start");
    return this.http.post<Category>(this.categoryUrl, category, this.httpOptions);
  };

  
  public handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  };
}
