import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'forbidden',
    template: `
        <h1><i class="fa fa-exclamation-triangle"></i> Access denied</h1>
        <h3>У вас нет доступа для просмотра этого раздела</h3>
    `
})
export class ForbiddenComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
}
