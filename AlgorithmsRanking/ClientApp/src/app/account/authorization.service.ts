import { Injectable } from '@angular/core';
import { AuthGroup } from './account.models';
import { AccountService } from './account.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthorizationService {
    permissions: string[];

    constructor(private _account: AccountService) { }

    hasPermission(authGroup: AuthGroup): boolean {
        if (!this.permissions) {
            this.initializePermissions();
        }
        return this.permissions && !!this.permissions.find(permission => permission === authGroup);
    }

    // This method is called once and a list of permissions is stored in the permissions property
    initializePermissions(): string[] {
        return this.permissions = this._account.getPermissions();
    }
}
