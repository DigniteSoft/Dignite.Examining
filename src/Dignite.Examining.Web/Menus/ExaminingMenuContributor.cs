using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Examining.Web.Menus
{
    public class ExaminingMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(ExaminingMenus.Prefix, displayName: "Examining", "~/Examining", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}