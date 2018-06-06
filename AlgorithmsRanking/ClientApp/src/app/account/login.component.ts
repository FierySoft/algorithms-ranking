import { Component, OnInit } from '@angular/core';

import { UserCredentials } from './account.models';
import { AccountService } from './account.service';

@Component({
    templateUrl: 'login.component.html',
    styles: [`
        .login-form {
            margin-top: 15%;
            padding: 20px;
            border: 1px solid;
            border-radius: 10px;
        }
    `]
})
export class AccountLoginComponent implements OnInit {
    cred: UserCredentials;

    constructor(private _account: AccountService) { }

    ngOnInit() {
        this.cred = new UserCredentials();
    }

    public login(): void {
        this._account.login(this.cred)
            .subscribe(
                result => {
                    this._account.storeUserInfo(result);
                    window.location.href = '/';
                },
                error => console.log(error)
            );
    }
}
