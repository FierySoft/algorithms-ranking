import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

import { Person } from '../app.models';

@Component({
    selector: 'person-card',
    template: `
        <span [popover]="personPopover" [popoverOnHover]="false">
            <b><i class="fa fa-user-o"></i> {{ label }}:</b>
            {{ value.shortName }}
        </span>
        <popover-content #personPopover [title]="value.fullName" placement="bottom" [closeOnClickOutside]="true">
            <p><i class="fa fa-envelope-o"></i> <a [attr.href]="'mailto:' + value.email">{{ value.email }}</a></p>
            <p><i class="fa fa-phone"></i> <a [attr.href]="'tel:' + value.phone">{{ value.phone }}</a></p>
        </popover-content>
    `
})
export class PersonCardComponent {
    @Input() value: Person;
    @Input() label: string = 'Участник';

    constructor(private _sanitizer: DomSanitizer) { }

    sanitize = (url: string) => this._sanitizer.bypassSecurityTrustUrl(url);
}
