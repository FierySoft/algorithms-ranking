import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthorizationService } from '../authorization.service';
import { AuthGroup } from '../account.models';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        protected _router: Router,
        protected _auth: AuthorizationService
    ) { }

    canActivate = (route: ActivatedRouteSnapshot): Observable<boolean> | boolean => {
        if (!this.hasRequiredPermission(route.data['auth'])) {
            this._router.navigate(['forbidden']);
            return false;
        }
        return true;
    }

    protected hasRequiredPermission = (authGroup: AuthGroup): Observable<boolean> | boolean => {
        return authGroup ?
            this._auth.hasPermission(authGroup) :
            this._auth.hasPermission(null);
    }
}
