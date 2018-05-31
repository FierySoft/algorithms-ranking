import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';

import { ResearchesRoutingModule, researchesComponents } from './researches.routing';
import { ResearchesService } from './researches.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        ResearchesRoutingModule
    ],
    declarations: [
        ...researchesComponents
    ],
    providers: [
        ResearchesService
    ],
    exports: [

    ]
})
export class ResearchesModule {

}
