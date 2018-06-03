import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Comment } from '../researches.models';

@Injectable()
export class CommentsService {
    private _url: string;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') baseUrl
    ) {
        this._url = baseUrl + 'api/comments';
    }

    public getCommentsForResearch = (researchId: number): Observable<Comment[]> =>
        this._http.get<Comment[]>(`${this._url}/${researchId}`)

    public postComment = (researchId: number, content: string): Observable<Comment> =>
        this._http.post<Comment>(`${this._url}/${researchId}`, { content: content })
}
