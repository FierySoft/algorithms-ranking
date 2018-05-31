import { Component, OnInit } from '@angular/core';

import { ResearchesService } from './researches.service';
import { Research } from './researches.models';

@Component({
    templateUrl: 'list.component.html'
})
export class ResearchesListComponent implements OnInit {
    items: Research[];

    constructor(private _researches: ResearchesService) { }

    ngOnInit() {
        this._researches.getResearches()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    public selectItem(id: number): void {
        if (!id) { return; }
        this._researches.gotoItem(id);
    }

    public deleteItem(item: Research) {
        if (!item) { return; }
        if (confirm(`Вы действительно хотите удалить \'${item.name}\'?`)) {
            this._researches.deleteResearch(item.id);
        }
    }

    public createItem() {
        this._researches.gotoCreate();
    }
}
