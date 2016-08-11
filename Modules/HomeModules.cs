using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class Homemodule : NancyModule
  {
    public Homemodule()
    {
      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/stylists"] = _ => {
          List<Stylist> AllStylists = Stylist.GetAll();
          return View["stylists.cshtml", AllStylists];
        };

      Get["/stylists/new"] = _ => {
        return View["stylists_form.cshtml"];
      };

      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        return View["success.cshtml"];
      };

        Get["stylist/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", SelectedStylist];
      };

      Patch["stylist/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"]);
        return View["success.cshtml"];
      };

      Get["stylist/delete/{id}"] = parameters => {
      Stylist SelectedStylist = Stylist.Find(parameters.id);
      return View["stylist_delete.cshtml", SelectedStylist];
    };
      Delete["stylist/delete/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();
        return View["success.cshtml"];
    };


    }
  }
}
