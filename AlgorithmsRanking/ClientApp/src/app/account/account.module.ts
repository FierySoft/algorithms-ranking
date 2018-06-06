import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';

import { AccountService } from './account.service';
import { AccountRoutingModule, accountComponents } from './account.routing';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        FormsModule,
        AccountRoutingModule
    ],
    declarations: [
        ...accountComponents
    ],
    providers: [
        AccountService
    ],
    exports: [

    ]
})
export class AccountModule {

}
