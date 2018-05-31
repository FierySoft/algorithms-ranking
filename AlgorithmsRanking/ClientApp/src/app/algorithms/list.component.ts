import { Component, OnInit } from '@angular/core';

import { AlgorithmsService } from './algorithms.service';
import { Algorithm } from './algorithms.models';

@Component({
    selector: 'algorithms',
    templateUrl: 'list.component.html'
})
export class AlgorithmsListComponent implements OnInit {
    items: Algorithm[];

    constructor(private _algorithms: AlgorithmsService) { }

    ngOnInit() {
        this._algorithms.getAlgorithms()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    public selectItem(id: number): void {
        if (!id) { return; }
        this._algorithms.gotoItem(id);
    }

    public deleteItem(item: Algorithm) {
        if (!item) { return; }
        if (confirm(`Вы действительно хотите удалить \'${item.name}\'?`)) {
            this._algorithms.deleteAlgorithm(item.id);
        }
    }

    public createItem() {
        this._algorithms.gotoCreate();
    }
}
