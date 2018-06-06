import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccountComponent } from './account.component';
import { AccountLoginComponent } from './login.component';


const routes: Routes = [
    {
        path: 'account',
        component: AccountComponent,
        children: [
            { path: 'login', component: AccountLoginComponent },
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
export class AccountRoutingModule {

}


export const accountComponents = [
    AccountComponent,
    AccountLoginComponent
];
