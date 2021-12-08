import { Component, OnInit } from '@angular/core';
import { ReplaceableComponentsService } from '@abp/ng.core'; // imported ReplaceableComponentsService
import { RoutesComponent } from './routes/routes.component'; // imported RoutesComponent
import { eThemeBasicComponents } from '@abp/ng.theme.basic'; // imported eThemeBasicComponents

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent implements OnInit {
  constructor( private replaceableComponents: ReplaceableComponentsService) {} // injected ReplaceableComponentsService

  ngOnInit() {
    this.replaceableComponents.add({
        component: RoutesComponent,
        key: eThemeBasicComponents.Routes,
      });
  }
}
