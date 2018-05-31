import { Component, OnInit } from '@angular/core';

import { AccountService, UserInfo } from '../account';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    userInfo: UserInfo;
    isExpanded = false;

    constructor(private _account: AccountService) {}

    ngOnInit() {
        this.userInfo = this._account.retrieveUserInfo();

        if (!this.userInfo) {
            this._account.whoAmI()
                .subscribe(
                    result => { this.userInfo = result; console.log(result); },
                    error => console.log(error)
                );
        }
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
