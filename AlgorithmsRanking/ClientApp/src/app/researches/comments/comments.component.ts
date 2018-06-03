import { Component, Input, OnInit } from '@angular/core';

import { Comment } from '../researches.models';
import { CommentsService } from '../comments/comments.service';

@Component({
    selector: 'comments',
    template: `
        <fieldset class="col-md-6">
            <legend>Обсуждение ({{ comments ? comments.length : 0 }})</legend>
            <comments-list [items]="comments"></comments-list>
            <comment-create (post)="postComment($event)"></comment-create>
        </fieldset>
    `
})
export class CommentsComponent implements OnInit {
    @Input() researchId: number;
    comments: Comment[];

    constructor(private _comments: CommentsService) { }

    ngOnInit() {
        this._comments.getCommentsForResearch(this.researchId)
            .subscribe(
                result => this.comments = result,
                error => console.log(error)
            );
    }

    public postComment(content: string) {
        this._comments.postComment(this.researchId, content)
            .subscribe(
                result => { console.log(result); this.comments.push(result); },
                error => console.log(error)
            );
    }
}
