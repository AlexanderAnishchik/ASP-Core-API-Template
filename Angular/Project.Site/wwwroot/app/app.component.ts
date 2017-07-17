import { Component } from '@angular/core';

@Component({
    template: '<div>{{user}} angular4</div>',
    selector: 'app'
})
export class AppComponent {
    public user: string = "Test";
}