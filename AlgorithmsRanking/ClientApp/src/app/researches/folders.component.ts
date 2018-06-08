import { Component, OnInit } from '@angular/core';

import { ResearchesService } from './researches.service';
import { Research, ResearchFolders } from './researches.models';

@Component({
    selector: 'researches-folders',
    templateUrl: 'folders.component.html'
})
export class ResearchesFoldersComponent implements OnInit {
    folders: ResearchFolders;

    constructor(private _researches: ResearchesService) { }

    ngOnInit() {
        this._researches.getResearchFolders()
            .subscribe(
                result => this.folders = result,
                error => console.log(error)
            );
    }

    public createItem() {
        this._researches.gotoCreate();
    }
}
