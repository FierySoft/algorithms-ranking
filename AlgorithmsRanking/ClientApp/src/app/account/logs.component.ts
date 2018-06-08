import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { AccountService } from './account.service';
import { AccountActivity } from './account.models';

@Component({
    templateUrl: 'logs.component.html'
})
export class LogsListComponent implements OnInit {
    items: AccountActivity[];

    constructor(
        private _account: AccountService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => {
                const accountId = !!params['accountId'] ? +params['accountId'] : this._account.retrieveUserInfo().id;
                return this._account.getActivitiesList(accountId);
            })
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    public onSorted(event) {
        const col = event.sortColumn as string;
        const dir = event.sortDirection as string;

        this.items.sort((a: AccountActivity, b: AccountActivity) => {
            return dir === 'asc' ?
                a[col] === b[col] ? 0 : a[col] > b[col] ? 1 : -1 :
                a[col] === b[col] ? 0 : a[col] > b[col] ? -1 : 1;

        });
    }
}
