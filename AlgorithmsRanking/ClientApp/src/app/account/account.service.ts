import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { UserInfo, UserCredentials } from './account.models';

@Injectable()
export class AccountService {
    private _url: string;
    private _storage: Storage;

    constructor(
        @Inject('BASE_URL') _baseUrl: string,
        private _http: HttpClient,
        private _router: Router
    ) {
        this._url = _baseUrl + 'api/account';
        this._storage = localStorage; //sessionStorage;
    }

    public whoAmI = (): Observable<UserInfo> =>
        this._http.get<UserInfo>(`${this._url}/who-am-i`)
            .map((userInfo: UserInfo) => {
                this.storeUserInfo(userInfo);
                return userInfo;
            })


    public storeUserInfo(value: UserInfo) {
        this._storage.setItem('userInfo', JSON.stringify(value));
    }

    public retrieveUserInfo(): UserInfo {
        return JSON.parse(this._storage.getItem('userInfo')) as UserInfo;
    }

    public removeUserInfo(): void {
        this._storage.removeItem('userInfo');
    }


    public gotoHome(): void {
        this._router.navigate(['home']);
    }
}
