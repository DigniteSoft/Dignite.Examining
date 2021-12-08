import { Component } from '@angular/core';
import { ABP,RoutesService,TreeNode,PermissionService  } from '@abp/ng.core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-navs',
  templateUrl: './navs.component.html'
})
export class NavsComponent {

  constructor(
    private routesService: RoutesService,
    private permissionService: PermissionService,
    private route: ActivatedRoute) {
  }

  getRootRoute(){    
    var routes;
    this.routesService.visible$.subscribe((res: any) => {
      routes=res;
    });
    var url = this.route.url;
    debugger
    return routes;
  }
}
