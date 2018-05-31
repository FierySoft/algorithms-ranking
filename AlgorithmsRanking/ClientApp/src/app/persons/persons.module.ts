import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';

import { PersonsRoutingModule, personsComponents } from './persons.routing';
import { PersonsService } from './persons.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
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
