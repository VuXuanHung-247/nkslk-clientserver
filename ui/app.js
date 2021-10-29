const routes=[
    {path:'/home',component:home},
    {path:'/nkslk',component:nkslk},
    {path:'/sanpham',component:sanpham},
    {path:'/congviec',component:congviec},
    {path:'/congnhan',component:congnhan},
    {path:'/phongban',component:phongban},
    {path:'/report',component:report}
]

const router=new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')