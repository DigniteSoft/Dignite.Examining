import { Component, HostBinding,TrackByFunction  } from '@angular/core';
import { ABP,RoutesService,TreeNode,PermissionService  } from '@abp/ng.core';

@Component({
  selector: 'app-routes',
  templateUrl: './routes.component.html'
})
export class RoutesComponent {
  @HostBinding('class.mx-auto')
  marginAuto = true;  
  
  trackByFn: TrackByFunction<TreeNode<ABP.Route>> = (_, item) => {
    item.name;
  }

  constructor(
    public readonly routesService: RoutesService,
    private permissionService: PermissionService) {
  }

  get smallScreen() {
    return window.innerWidth < 992;
  }
  
  isDropdown(node: TreeNode<ABP.Route>) {
    console.log("输出菜单："+node.name);
    return !node?.isLeaf || this.routesService.hasChildren(node.name);
  }

  /* 检查子菜单是否有权限 */
  isGranted(node: TreeNode<ABP.Route>){
    for(let route of node.children){
      if(this.permissionService.getGrantedPolicy(
        route.requiredPolicy
      )){
        return true;
      }
    }
    return false;
  }

  /* 获取导航的路由地址 */
  /* 如果当前路由地址为 undefinde，则到子路由中查找 */
  getRoutePath(node: TreeNode<ABP.Route>){
    if(node.path!==undefined){
      return node.path;
    }
    else{
      for(let route of node.children){
        if(route.path!==undefined){
          return route.path;
        }
        else
        {
          return this.getRoutePath(route);
        }
      }
    }
  }
}