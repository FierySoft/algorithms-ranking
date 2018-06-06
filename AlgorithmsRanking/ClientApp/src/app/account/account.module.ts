import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';

import { AuthGuard } from './guards/auth.guard';
import { AccountService } from './account.service';
import { AuthorizationService } from './authorization.service';
import { AccountRoutingModule, accountComponents } from './account.routing';
import { DisableIfUnauthorizedDirective } from './directives/disable-if-unauthorized.directive';
import { HideIfUnauthorizedDirective, HideIfUnauthorizedOrDirective } from './directives/hide-if-unauthorized.directive';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        FormsModule,
        AccountRoutingModule
    ],
    declarations: [
        ...accountComponents,
        DisableIfUnauthorizedDirective,
        HideIfUnauthorizedDirective,
        HideIfUnauthorizedOrDirective
    ],
    providers: [
        AuthGuard,
        AccountService,
        AuthorizationService
    ],
    exports: [
        DisableIfUnauthorizedDirective,
        HideIfUnauthorizedDirective,
        HideIfUnauthorizedOrDirective
    ]
})
export class AccountModule {

}
