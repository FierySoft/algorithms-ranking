import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Person } from './persons.models';

@Injectable()
export class PersonsService {
    private _url: string;

    constructor(
        @Inject('BASE_URL') baseUrl: string,
        private _http: HttpClient,
        private _router: Router
    ) {
        this._url = baseUrl + 'api/persons';
    }


    public getPersons = (): Observable<Person[]> =>
        this._http.get<Person[]>(this._url)

    public getPerson = (id: number): Observable<Person> =>
        this._http.get<Person>(`${this._url}/${id}`)

    public postPerson = (value: Person): Observable<Person> =>
        this._http.post<Person>(`${this._url}`, value)

    public putPerson = (id: number, value: Person): Observable<Person> =>
        this._http.put<Person>(`${this._url}/${id}`, value)

    public deletePerson = (id: number): any =>
        this._http.delete<any>(`${this._url}/${id}`)
            .subscribe(
                result => window.location.reload(),
                error => console.log(error)
            )


    public gotoList(): void {
        this._router.navigate(['persons']);
    }

    public gotoItem(id: number): void {
        this._router.navigate(['persons', id]);
    }

    public gotoCreate(): void {
        this._router.navigate(['persons', 'create']);
    }
}
