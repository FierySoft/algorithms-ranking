import { Component, Input } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

export class ApiError {
    public code: string = '00000';
    public message: string = 'Во время обработки Вашего запроса возникла проблема.';
    public detail: string = `
        Попробуйте повторить запрос через несколько минут.
        Если ошибка повторилась, обратитесь в Службу поддержки.`;
    public data: { id: number, name: string }[];

    constructor(code: number | string, message: string, detail: string, data: { id: number, name: string }[] = null) {
        this.code = code.toString();
        this.message = message;
        this.detail = detail;
        this.data = data;
    }

    public static fromHttpErrorResponse(error: HttpErrorResponse): ApiError {
        return new ApiError(error.status, error.statusText, error.message);
    }
}


@Component({
    selector: 'error',
    template: `
        <div *ngIf="active" class="panel panel-danger">
            <div class="alert alert-danger panel-body" [class.alert-dismissible]="dismissible" role="alert" style="margin-bottom:0">
                <div class="col-md-1 pull-right">
                    <button *ngIf="dismissible"
                        (click)="active=!active"
                        type="button"
                        class="close"
                        data-dismiss="alert"
                        aria-label="Close">
                        <i class="fa fa-close" style="padding:0 20px"></i>
                    </button>
                </div>
                <div class="col-md-1 text-center">
                    <i class="fa fa-warning fa-5x" style="margin-top:20px 0"></i>
                </div>
                <div class="col-md-10 text-center">
                    <h3 style="margin-top:0">{{ error.code }}: {{ error.message }}</h3>
                    <h4>{{ error.detail }}</h4>
                    <div *ngIf="refreshable">
                        <a (click)="refresh()" style="cursor:pointer">
                            <h4>
                                <span class="fa fa-refresh"></span>
                                Обновить страницу
                            </h4>
                        </a>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div *ngIf="error.data">
                    <input type="checkbox" #showData [hidden]="true" >
                    <h5 (click)="showData.checked=!showData.checked" class="text-center" style="cursor:pointer">
                        Зависимости ({{ error.data?.length || 0 }}) 
                        <i class="fa" [class.fa-eye]="showData.checked" [class.fa-eye-slash]="!showData.checked"></i>
                    </h5>
                    <table *ngIf="showData.checked" class="table table-condensed">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Значение</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr *ngFor="let d of error.data">
                                <td>{{ d.id }}</td>
                                <td>{{ d.name }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    `
})
export class ErrorComponent {
    @Input() active: boolean;
    @Input() dismissible: boolean = false;
    @Input() refreshable: boolean = true;
    @Input() error: ApiError;

    constructor() { }

    refresh() {
        window.location.reload();
    }
}
