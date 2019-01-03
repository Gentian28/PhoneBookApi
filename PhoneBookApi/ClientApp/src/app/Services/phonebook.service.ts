import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { PhoneBook } from './PhoneBookItem';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })

export class PhoneBookService {

  private PhoneBookItemsUrl = 'api/phonebook';  // URL to web api

  constructor(private http: HttpClient) { }

  public phonebook: PhoneBook[];

  /** GET phonebook from server. */
  getPhoneBookItems(): Observable<PhoneBook[]> {
    return this.http.get<PhoneBook[]>(this.PhoneBookItemsUrl);
  }

  /** GET contact by id. */
  getPhoneBookItem(id: number): Observable<PhoneBook> {
    const url = `${this.PhoneBookItemsUrl}/${id}`;
    return this.http.get<PhoneBook>(url);
  }

  /** POST: add new contact to server */
  addContact(contact: PhoneBook): Observable<PhoneBook> {
    return this.http.post<PhoneBook>(this.PhoneBookItemsUrl, contact, httpOptions).pipe(
      tap(() => catchError(this.handleError<PhoneBook>('addContact')))
    );
  }

  /** PUT: update contact on server */
  updataContact(id: number, contact: PhoneBook): Observable<any> {
    return this.http.put(`${this.PhoneBookItemsUrl}/${id}`, contact, httpOptions);
  }

  /** DELETE: delete contact from server */
  deleteContact(id: number): Observable<PhoneBook> {
    return this.http.delete<PhoneBook>(`${this.PhoneBookItemsUrl}/${id}`, httpOptions);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
    
      console.error(error); // log to console

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
