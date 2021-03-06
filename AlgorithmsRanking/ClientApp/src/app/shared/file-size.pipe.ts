import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'fileSize'})
export class FileSizePipe implements PipeTransform {
    private units = [
        'bytes',
        'Kb',
        'Mb',
        'Gb',
        'Tb',
        'Pb'
    ];

    transform(bytes: number = 0, precision: number = 2 ): string {
        if (isNaN(parseFloat(bytes.toString())) || ! isFinite(bytes)) { return '?'; }

        let unit = 0;

        while (bytes >= 1024) {
            bytes /= 1024;
            unit ++;
        }

        return bytes.toFixed(+ precision) + ' ' + this.units[unit];
    }
}
