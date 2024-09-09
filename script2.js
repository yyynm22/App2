new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data() {
        return {
            tab: 0, // 初期タブ
            userData: {
                user_id: '',
                user_name: '',
                user_pass: '',
                user_postcode: '',
                user_adress: '',
                user_telenum: ''
            }, // ログインユーザーの会員登録情報を格納するオブジェクト
        };
    },
    methods: {
        fetchUserData() {
            // sessionStorageからユーザー情報を取得
            this.userData.user_name = sessionStorage.getItem('user_name');
            this.userData.user_pass = sessionStorage.getItem('user_pass');
            this.userData.user_mail = sessionStorage.getItem('user_mail');
            this.userData.user_postcode = sessionStorage.getItem('user_postcode');
            this.userData.user_adress = sessionStorage.getItem('user_adress');
            this.userData.user_telenum = sessionStorage.getItem('user_telenum');
            
            // 任意でuser_idを設定（ここでは例としてメールアドレスを使用）
            this.userData.user_id = sessionStorage.getItem('user_mail'); 
        },
        addData() {
            //商品の検索画面に遷移
            window.location.href = '/index1.html';
        },
    },
    mounted() {
        // マウント時にユーザーデータを取得
        this.fetchUserData();
    },
});
