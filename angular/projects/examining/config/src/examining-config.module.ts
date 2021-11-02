import { ModuleWithProviders, NgModule } from '@angular/core';
import { EXAMINING_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class ExaminingConfigModule {
  static forRoot(): ModuleWithProviders<ExaminingConfigModule> {
    return {
      ngModule: ExaminingConfigModule,
      providers: [EXAMINING_ROUTE_PROVIDERS],
    };
  }
}
