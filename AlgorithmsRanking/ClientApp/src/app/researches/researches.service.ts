import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Research, ResearchForm, ResearchInitForm, ResearchCalculatedForm } from './researches.models';

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

    public getResearchCreate = (): Observable<ResearchForm> =>
        this._http.get<ResearchForm>(`${this._url}/create`)

    public postResearch = (value: ResearchInitForm): Observable<Research> =>
        this._http.post<Research>(`${this._url}`, value)

    public getResearchEdit = (id: number): Observable<ResearchForm> =>
        this._http.get<ResearchForm>(`${this._url}/${id}/edit`)

    public putResearch = (id: number, value: ResearchInitForm): Observable<Research> =>
        this._http.put<Research>(`${this._url}/${id}`, value)

    public deleteResearch = (id: number): any =>
        this._http.delete<any>(`${this._url}/${id}`)
            .subscribe(
                result => window.location.reload(),
                error => console.log(error)
            )

    public startResearch = (id: number): Observable<Research> =>
        this._http.get<Research>(`${this._url}/${id}/start`)

    public executeResearch = (id: number, value: ResearchCalculatedForm): Observable<Research> =>
        this._http.post<Research>(`${this._url}/${id}/execute`, value)

    public declineResearch = (id: number): Observable<Research> =>
        this._http.get<Research>(`${this._url}/${id}/decline`)

    public closeResearch = (id: number): Observable<Research> =>
        this._http.get<Research>(`${this._url}/${id}/close`)


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
