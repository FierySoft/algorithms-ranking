import { Directive, ElementRef, OnInit, Input } from '@angular/core';
import { AuthorizationService } from '../authorization.service';
import { AuthGroup } from '../account.models';
import { forEach } from '@angular/router/src/utils/collection';

@Directive({
    selector: '[hideIfUnauthorized]'
})
export class HideIfUnauthorizedDirective implements OnInit {
    @Input('hideIfUnauthorized') permission: AuthGroup; // Required permission passed in

    constructor(private el: ElementRef, private _auth: AuthorizationService) { }

    ngOnInit() {
        if (!this._auth.hasPermission(this.permission)) {
            this.el.nativeElement.style.display = 'none';
        }
    }
}


@Directive({
    selector: '[hideIfUnauthorizedOr]'
})
export class HideIfUnauthorizedOrDirective implements OnInit {
    @Input('hideIfUnauthorizedOr') permissions: AuthGroup[]; // Required permission passed in

    constructor(private el: ElementRef, private _auth: AuthorizationService) { }

    ngOnInit() {
        let show = false;

        for (let i = 0; i < this.permissions.length; i++) {
            if (this._auth.hasPermission(this.permissions[i])) {
                show = true;
                break;
            }
        }

        if (!show) {
            this.el.nativeElement.style.display = 'none';
        }
    }
}
