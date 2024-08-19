const app = new Vue({
  el: '#app', // Vueが管理する一番外側のDOM要素
  vuetify: new Vuetify(),
    data: {
        temperature: '',
        season: '',
        dress: '',
        dressURL: '',
        mark: '',
        dataList: [], // データ表示用配列
        showImages: false // 画像表示のフラグを追加
    },

  methods: {
    // DBにデータを追加する関数
    addData: async function() {
      // IDの入力チェック（空白か数字以外なら終了）
      if(!this.temperature || isNaN(this.temperature)){
        console.log("temperatureに数値が入力されていません");
        return;
      }
      
      // POSTメソッドで送るパラメーターを作成
      const param = {
        temperature : this.temperature,
        season: this.season,
        dress : this.dress,
        dressURL: this.dressURL,
        mark:this.mark,
      };
      
      // INSERT用のAPIを呼び出し
      const response = await axios.post('https://m3h-yuunaminagawa.azurewebsites.net/api/INSERT', param);
      
      // 結果をコンソールに出力
      console.log(response.data);
      
      // 保存が完了したらフィールドをクリア
      this.temperature = '';
      this.season = '';
      this.dress = '';
      this.dressURL = '';
      this.mark = '';
    },
    
    // データベースからデータを取得する関数
      readData: async function () {
          // SELECT用のAPIを呼び出し      
          const response = await axios.get('https://m3h-yuunaminagawa.azurewebsites.net/api/SELECT');

          // 結果をコンソールに出力
          console.log(response.data);

          // 結果リストを表示用配列に代入
          this.dataList = response.data.List;

          // 画像表示を有効にする
          this.showImages = true;
      },

  },
});

document.addEventListener('DOMContentLoaded', function() {
  // タブに対してクリックイベントを適用
  const tabs = document.getElementsByClassName('tab');
  for(let i = 0; i < tabs.length; i++) {
    tabs[i].addEventListener('click', tabSwitch, false);
  }

  // タブをクリックすると実行する関数
  function tabSwitch() {
    // タブのclassの値を変更
    document.getElementsByClassName('is-active')[0].classList.remove('is-active');
    this.classList.add('is-active');
    
    // コンテンツのclassの値を変更
    document.getElementsByClassName('is-show')[0].classList.remove('is-show');
    const arrayTabs = Array.prototype.slice.call(tabs);
    const index = arrayTabs.indexOf(this);
    document.getElementsByClassName('panel')[index].classList.add('is-show');
  };
}, false);