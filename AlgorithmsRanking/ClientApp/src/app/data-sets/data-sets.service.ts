import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { DataSet } from './data-sets.models';

@Injectable()
export class DataSetsService {
    private _url: string;

    constructor(
        @Inject('BASE_URL') baseUrl: string,
        private _http: HttpClient,
        private _router: Router
    ) {
        this._url = baseUrl + 'api/datasets';
    }


    public getDataSets = (): Observable<DataSet[]> =>
        this._http.get<DataSet[]>(this._url)

    public getDataSet = (id: number): Observable<DataSet> =>
        this._http.get<DataSet>(`${this._url}/${id}`)

    public postDataSet = (value: DataSet): Observable<DataSet> =>
        this._http.post<DataSet>(`${this._url}`, value)

    public putDataSet = (id: number, value: DataSet): Observable<DataSet> =>
        this._http.put<DataSet>(`${this._url}/${id}`, value)

    public deleteDataSet = (id: number): any =>
        this._http.delete<any>(`${this._url}/${id}`)
            .subscribe(
                result => window.location.reload(),
                error => console.log(error)
            )


    public gotoList(): void {
        this._router.navigate(['data-sets']);
    }

    public gotoItem(id: number): void {
        this._router.navigate(['data-sets', id]);
    }

    public gotoCreate(): void {
        this._router.navigate(['data-sets', 'create']);
    }
}
