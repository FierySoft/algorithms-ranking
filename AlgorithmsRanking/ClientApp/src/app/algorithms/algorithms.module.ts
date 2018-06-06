import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';
import { AccountModule } from '../account/account.module';

import { AlgorithmsRoutingModule, algorithmsComponents } from './algorithms.routing';
import { AlgorithmsService } from './algorithms.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        AccountModule,
        AlgorithmsRoutingModule
    ],
    declarations: [
        ...algorithmsComponents
    ],
    providers: [
        AlgorithmsService
    ],
    exports: [

    ]
})
export class AlgorithmsModule {

}
