import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';
import { AccountModule } from '../account/account.module';

import { ResearchesRoutingModule, researchesComponents } from './researches.routing';
import { ResearchesService } from './researches.service';
import { CommentsService } from './comments/comments.service';
import { ResearchesFoldersComponent } from './folders.component';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        AccountModule,
        ResearchesRoutingModule
    ],
    declarations: [
        ...researchesComponents
    ],
    providers: [
        ResearchesService,
        CommentsService
    ],
    exports: [
        ResearchesFoldersComponent
    ]
})
export class ResearchesModule {

}
