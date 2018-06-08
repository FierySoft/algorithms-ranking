import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccountComponent } from './account.component';
import { AccountLoginComponent } from './login.component';
import { LogsListComponent } from './logs.component';


const routes: Routes = [
    {
        path: 'account',
        component: AccountComponent,
        children: [
            { path: 'login', component: AccountLoginComponent },
            { path: 'logs/:accountId', component: LogsListComponent },
            { path: 'logs', component: LogsListComponent }
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
    AccountLoginComponent,
    LogsListComponent
];
