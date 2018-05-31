import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Algorithm } from './algorithms.models';

@Injectable()
export class AlgorithmsService {
    private _url: string;

    constructor(
        @Inject('BASE_URL') baseUrl: string,
        private _http: HttpClient,
        private _router: Router
    ) {
        this._url = baseUrl + 'api/algorithms';
    }


    public getAlgorithms = (): Observable<Algorithm[]> =>
        this._http.get<Algorithm[]>(this._url)

    public getAlgorithm = (id: number): Observable<Algorithm> =>
        this._http.get<Algorithm>(`${this._url}/${id}`)

    public postAlgorithm = (value: Algorithm): Observable<Algorithm> =>
        this._http.post<Algorithm>(`${this._url}`, value)

    public putAlgorithm = (id: number, value: Algorithm): Observable<Algorithm> =>
        this._http.put<Algorithm>(`${this._url}/${id}`, value)

    public deleteAlgorithm = (id: number): any =>
        this._http.delete<any>(`${this._url}/${id}`)
            .subscribe(
                result => window.location.reload(),
                error => console.log(error)
            )


    public gotoList(): void {
        this._router.navigate(['algorithms']);
    }

    public gotoItem(id: number): void {
        this._router.navigate(['algorithms', id]);
    }

    public gotoCreate(): void {
        this._router.navigate(['algorithms', 'create']);
    }
}
