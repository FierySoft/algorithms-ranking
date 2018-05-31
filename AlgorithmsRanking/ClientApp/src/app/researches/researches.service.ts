import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Research, ResearchUpdate } from './researches.models';

@Injectable()
export class ResearchesService {
    private _url: string;

    constructor(
        @Inject('BASE_URL') baseUrl: string,
        private _http: HttpClient,
        private _router: Router
    ) {
        this._url = baseUrl + 'api/researches';
    }


    public getResearches = (): Observable<Research[]> =>
        this._http.get<Research[]>(this._url)

    public getResearch = (id: number): Observable<Research> =>
        this._http.get<Research>(`${this._url}/${id}`)

    public postResearch = (value: ResearchUpdate): Observable<Research> =>
        this._http.post<Research>(`${this._url}`, value)

    public putResearch = (id: number, value: ResearchUpdate): Observable<Research> =>
        this._http.put<Research>(`${this._url}/${id}`, value)

    public deleteResearch = (id: number): any =>
        this._http.delete<any>(`${this._url}/${id}`)
            .subscribe(
                result => window.location.reload(),
                error => console.log(error)
            )


    public gotoList(): void {
        this._router.navigate(['researches']);
    }

    public gotoItem(id: number): void {
        this._router.navigate(['researches', id]);
    }

    public gotoCreate(): void {
        this._router.navigate(['researches', 'create']);
    }
}
