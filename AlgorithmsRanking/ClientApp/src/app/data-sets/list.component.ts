import { Component, OnInit } from '@angular/core';

import { DataSetsService } from './data-sets.service';
import { DataSet } from './data-sets.models';

@Component({
    selector: 'data-sets',
    templateUrl: 'list.component.html'
})
export class DataSetsListComponent implements OnInit {
    items: DataSet[];

    constructor(private _dataSets: DataSetsService) { }

    ngOnInit() {
        this._dataSets.getDataSets()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    public selectItem(id: number): void {
        if (!id) { return; }
        this._dataSets.gotoItem(id);
    }

    public deleteItem(item: DataSet) {
        if (!item) { return; }
        if (confirm(`Вы действительно хотите удалить \'${item.name}\'?`)) {
            this._dataSets.deleteDataSet(item.id);
        }
    }

    public createItem() {
        this._dataSets.gotoCreate();
    }
}
