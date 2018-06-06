import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';
import { AccountModule } from '../account/account.module';

import { PersonsRoutingModule, personsComponents } from './persons.routing';
import { PersonsService } from './persons.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        AccountModule,
        PersonsRoutingModule
    ],
    declarations: [
        ...personsComponents
    ],
    providers: [
        PersonsService
    ],
    exports: [

    ]
})
export class PersonsModule {

}
