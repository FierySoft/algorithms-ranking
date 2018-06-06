import { Directive, ElementRef, OnInit, Input } from '@angular/core';
import { AuthorizationService } from '../authorization.service';
import { AuthGroup } from '../account.models';

@Directive({
    selector: '[disableIfUnauthorized]'
})
export class DisableIfUnauthorizedDirective implements OnInit {
    @Input('disableIfUnauthorized') permission: AuthGroup; // Required permission passed in

    constructor(private el: ElementRef, private _auth: AuthorizationService) { }

    ngOnInit() {
        if (!this._auth.hasPermission(this.permission)) {
            this.el.nativeElement.disabled = true;
        }
    }
}
