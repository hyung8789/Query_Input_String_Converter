# Query Input String Converter
---

<p><img src="./res/icons8-sql-48.png"></p>
쿼리 입력 문자열 변환기

---

## < For What >
- 엑셀, 텍스트 파일 등으로부터 받은 입력 구분자 (\n, \t, ',', ' ' 등)로 구별되는 일련의 데이터 목록을 쿼리 조건식으로 입력을 위한 문자열 형식 (선행 및 후행에 단일 부호 혹은 이중 부호가 존재하는 문자 시퀸스) 으로 변환하기 위해, 입력 구분자 (\n, \t, ',', ' ' 등)으로 구별되는 각 데이터의 선행 및 후행에 문자열 형식에 사용되는 문자 추가

```sql
SELECT * FROM FooTable WHERE FooTable.Key IN ('FOO1', 'FOO2');
--쿼리문 등을 사용 시 일련의 데이터 목록으로부터 입력 위한 문자열로 변환이 필요 할 경우
```

```csharp
string[] fooArray = new string[] { "BAR1", "BAR2" };
char[] fooArray = new char[] { 'a', 'b', 'c' };
//일련의 문자열 목록 혹은 단일 문자 목록으로부터 입력 위한 문자열 혹은 문자로 변환이 필요 할 경우
```

---

## < settings.ini설정 방법 >

    1) INPUT_DELIMITER : 입력 파일의 각 문자열을 구분하기 위한 구분자(\n, \t, ',', ' ' 등)
        1-1) https://www.asciitable.com/ 의 10진 ASCII 코드 참조 할 것

    2) SQL_STR_SIGN_TYPE : 문자열 부호 종류
        2-1) 단일 부호 : '
        2-2) 이중 부호 : "

---

## < 명령줄 인수 사용법 >

쿼리 입력 문자열 변환기.exe (입력 파일명) (출력 파일명 (Optional), 입력하지 않을 경우 클립보드로 결과 복사)<br />
ex) 쿼리 입력 문자열 변환기.exe input.txt result.txt

---

## < System Requirement >
- .Net Framework 4.8

---

## < License >
[This application is licensed under the MIT License.](./LICENSE)</b><br /><br />