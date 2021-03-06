import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ResearchesComponent } from './researches.component';
import { ResearchStatusComponent } from './status.component';
import { ResearchFormComponent } from './form.component';
import { ResearchCardComponent } from './card.component';
import { ResearchesFoldersComponent } from './folders.component';
import { ResearchesListComponent } from './list.component';
import { ResearchesCreateComponent } from './create.component';
import { ResearchesEditComponent } from './edit.component';

import { CommentsComponent, CommentsListComponent, CommentCreateComponent } from './comments';


const routes: Routes = [
    {
        path: 'researches',
        component: ResearchesComponent,
        children: [
            { path: '', component: ResearchesListComponent },
            { path: 'create', component: ResearchesCreateComponent },
            { path: ':id', component: ResearchesEditComponent }
        ]
    },
];


@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ],
})
export class ResearchesRoutingModule {

}


export const researchesComponents = [
    ResearchesComponent,
    ResearchStatusComponent,
    ResearchFormComponent,
    ResearchCardComponent,
    ResearchesFoldersComponent,
    ResearchesListComponent,
    ResearchesCreateComponent,
    ResearchesEditComponent,
    CommentsComponent, CommentsListComponent, CommentCreateComponent
];
