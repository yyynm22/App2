<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <title>Kids Sabsc(product)</title>
  <link rel="stylesheet" href="./style1.css">
  <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css'>
  <link rel='stylesheet' href='https://unpkg.com/@mdi/font@6.x/css/materialdesignicons.min.css'>
  <link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900'>
</head>

<body>
  <!-- Vueで指定された"app"要素、この中でのみVueが有効 -->
  <div id="app">
    <v-app>
      <!-- ヘッダー -->
      <v-app-bar app class="custom-font" style="justify-content: space-between;">
        <v-btn @click="Logout" dark rounded class="custom-font" aria-label="ログアウト" title="ログアウト">
          <v-icon start>mdi-logout</v-icon>
          Logout
        </v-btn>

        <v-toolbar-title style="position: absolute; left: 50%; transform: translateX(-50%);">Kids Sabsc</v-toolbar-title>

        <div style="display: flex; margin-left: auto;">
          <v-btn v-on:click="cartdialog = true" dark rounded class="custom-font" style="margin-right: 16px;" aria-label="カート" title="カート">
            <v-icon start>mdi-cart</v-icon>
          </v-btn>

          <v-btn @click="mypage" dark color="#f09199" rounded class="custom-font" title="マイページ" aria-label="マイページ">
            <v-icon start>mdi-weather-night</v-icon>
            My page
          </v-btn>
        </div>
      </v-app-bar>

      <v-dialog v-model="cartdialog" max-width="600px">
        <v-card>
          <v-card-title class="subheading font-weight-bold">
            カート
            <v-spacer></v-spacer>
            <v-btn icon @click="cartdialog = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-card-title>
          <v-card-text>
            <v-row>
              <v-col v-for="item in cartItems" :key="item.product_id" cols="6">
                <div>{{ item.name }}</div>
                <div>{{ item.price }}円</div>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="red" text @click="cartdialog = false">注文確定</v-btn>
            <v-btn color="primary" text @click="cartdialog = false">閉じる</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <template>
  <v-main>
    <v-container>
      <!-- CategoryとKids genderの選択 -->
      <v-row class="d-flex justify-center align-center" style="width: 100%;">
        <v-col cols="12" sm="6" md="3">
          <v-select v-model="Category" label="Category" solo :items="['Tops', 'Pants', 'Skirt']"></v-select>
          <v-select v-model="Kidsgender" label="Kids gender" solo :items="['Boy', 'Girl', 'All']"></v-select>
        </v-col>
      </v-row>

      <!-- 検索ボタン -->
      <v-row class="d-flex justify-center align-center" style="width: 100%;">
        <v-col cols="auto">
          <v-btn v-on:click="readData1" dark color="#f09199" rounded class="custom-font" title="検索" aria-label="検索">SEARCH</v-btn>
        </v-col>
        <v-col cols="auto">
          <v-btn v-on:click="readData2" dark color="#f09199" rounded class="custom-font" title="一覧" aria-label="一覧">List</v-btn>
        </v-col>
      </v-row>

      <!-- dataList1の内容を表示 -->
<v-row class="d-flex justify-center align-center" style="width: 100%;">
  <v-col cols="12" sm="6" md="3" v-for="(item, index) in dataList1" :key="index">
    <!-- 商品カード -->
    <v-card class="mx-auto" max-width="344" outlined>
      <v-img :src="item.URL" aspect-ratio="1.5"></v-img>
      <v-card-title>{{ item.product_name }}</v-card-title>
      <v-card-subtitle>Category: {{ item.product_category }}</v-card-subtitle>
      <v-card-subtitle>Gender: {{ item.product_gender }}</v-card-subtitle>
      <v-card-actions>
        <v-btn :color="item.liked ? 'red' : 'grey'" @click="toggleLike(item)">
          <v-icon>{{ item.liked ? 'mdi-heart' : 'mdi-heart-outline' }}</v-icon>
        </v-btn>
        <v-btn :color="item.saved ? 'blue' : 'grey'" @click="toggleSave(item)">
          <v-icon>{{ item.saved ? 'mdi-content-save' : 'mdi-content-save-outline' }}</v-icon>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-col>

  <!-- 商品詳細ダイアログ -->
<v-dialog v-model="dialog" max-width="600px">
  <v-card>
    <v-card-title>
      <span class="headline">{{ selectedItem.product_name }}</span>
      <v-spacer></v-spacer>
      <v-btn icon @click="dialog = false">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    
    <v-card-text>
      <v-container>
        <!-- 商品画像 -->
        <v-row>
          <v-col cols="12">
            <v-img :src="selectedItem.URL" aspect-ratio="1.5"></v-img>
          </v-col>
        </v-row>
        <!-- 商品情報 -->
        <v-row>
          <v-col cols="12">
            <div>Category: {{ selectedItem.product_category }}</div>
            <div>Gender: {{ selectedItem.product_gender }}</div>
          </v-col>
        </v-row>
        <!-- サイズ選択 -->
        <v-row>
          <v-col cols="12">
            <v-select v-model="selectedSize" :items="['S', 'M', 'L', 'XL']" label="Select Size"></v-select>
          </v-col>
        </v-row>
        <!-- 個数入力 -->
        <v-row>
          <v-col cols="12">
            <v-text-field v-model="selectedQuantity" label="Quantity" type="number"></v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    
    <v-card-action>
      <v-spacer></v-spacer>
      <!-- カートに追加 -->
      <v-btn @click="addToCart(selectedItem, selectedSize, selectedQuantity)">
        Add to Cart
      </v-btn>
      <!-- ダイアログを閉じる -->
      <v-btn @click="dialog = false">
        Close
      </v-btn>
    </v-card-action>
  </v-card>
</v-dialog>

</v-row>




      
      <!-- dataList2の内容を表示 -->
      <v-row class="d-flex justify-center align-center" style="width: 100%;">
  <v-col cols="12" sm="6" md="3" v-for="(item, index) in dataList2" :key="index">
    <v-card class="mx-auto" max-width="344" outlined>
      <!-- 商品画像を表示 -->
      <v-img :src="item.URL" aspect-ratio="1.5" @click="selectedItem = item; dialog = true"></v-img>
      
      <v-card-title>{{ item.product_name }}</v-card-title>
      <v-card-subtitle>Category: {{ item.product_category }}</v-card-subtitle>
      <v-card-subtitle>Gender: {{ item.product_gender }}</v-card-subtitle>

      <v-card-actions>
        <v-btn :color="item.liked ? 'red' : 'grey'" @click="toggleLike(item)">
          <v-icon>{{ item.liked ? 'mdi-heart' : 'mdi-heart-outline' }}</v-icon>
        </v-btn>
        <v-btn :color="item.saved ? 'blue' : 'grey'" @click="toggleSave(item)">
          <v-icon>{{ item.saved ? 'mdi-content-save' : 'mdi-content-save-outline' }}</v-icon>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-col>
</v-row>

    </v-container>
  </v-main>
</template>

      <!-- フッター -->
      <v-footer app padless class="custom-font">
        <v-col class="text-center" cols="12">
          © 2024 TeamA
        </v-col>

        <v-btn dark fab fixed bottom right large elevation-6 title="一番上にスクロール" aria-label="一番上にスクロール" color="#f09199">
          <v-icon>mdi-chevron-up</v-icon>
        </v-btn>
      </v-footer>
    </v-app>
  </div>
  <!-- partial -->
  <script src='https://cdnjs.cloudflare.com/ajax/libs/vue/2.6.11/vue.min.js'></script>
  <script src='https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.js'></script>
  <script src='https://cdnjs.cloudflare.com/ajax/libs/axios/1.7.2/axios.min.js'></script>
  <script src="./script1.js"></script>
</body>

</html>
