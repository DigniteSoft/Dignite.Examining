import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { ExaminingComponent } from './components/examining.component';
import { ExaminingRoutingModule } from './examining-routing.module';

@NgModule({
  declarations: [ExaminingComponent],
  imports: [CoreModule, ThemeSharedModule, ExaminingRoutingModule],
  exports: [ExaminingComponent],
})
export class ExaminingModule {
  static forChild(): ModuleWithProviders<ExaminingModule> {
    return {
      ngModule: ExaminingModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<ExaminingModule> {
    return new LazyModuleFactory(ExaminingModule.forChild());
  }
}
