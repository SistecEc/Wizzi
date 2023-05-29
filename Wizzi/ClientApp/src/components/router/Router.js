import React from "react";
import { Switch, Route } from "react-router-dom";

import routes from "./routes"

//PAGES
import { NotFound } from "../pages/404";

export const Router = () => {
    return (
        <Switch>
            {routes.map((route, i) =>
                <Route key={i} exact={route.subRoutes.some(r => r.exact)} path={route.subRoutes.map(r => r.path)}>
                    <route.layout>
                        {route.subRoutes.map((subroute, i) =>
                            <route.routeComponent key={i} {...subroute} />
                        )}
                    </route.layout>
                </Route>
            )}
            <Route component={NotFound} />
        </Switch>
    );
}