import { Component, Input, OnInit } from '@angular/core';

import { ResearchesService } from './researches.service';
import { Research } from './researches.models';

@Component({
    selector: 'researches-list',
    templateUrl: 'list.component.html'
})
export class ResearchesListComponent implements OnInit {
    @Input() items: Research[] = [];

    constructor(private _researches: ResearchesService) { }

    ngOnInit() {
        /*this._researches.getResearches()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );*/
    }

    public onSorted(event) {
        const col = event.sortColumn as string;
        const dir = event.sortDirection as string;

        this.items.sort((a: Research, b: Research) => {
            return dir === 'asc' ?
                a[col] === b[col] ? 0 : a[col] > b[col] ? 1 : -1 :
                a[col] === b[col] ? 0 : a[col] > b[col] ? -1 : 1;

        });
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
}
